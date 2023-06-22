using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Common.Data.DataStore;
using PlayFab;
using PlayFab.ClientModels;
using UniEx;

namespace Furu.Common.Domain.Repository
{
    public sealed class PlayFabRepository
    {
        public PlayFabRepository()
        {
            PlayFabSettings.staticSettings.TitleId = PlayFabConfig.TITLE_ID;
        }

        public async UniTask<(string, LoginResult)> CreateUserAsync(CancellationToken token)
        {
            while (true)
            {
                var uid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
                var response = await LoginUserAsync(uid, token);

                // 新規作成できなければ、uidを再生成する
                if (response.NewlyCreated)
                {
                    return (uid, response);
                }

                await UniTask.Yield(token);
            }
        }

        public async UniTask<LoginResult> LoginUserAsync(string uid, CancellationToken token)
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = uid,
                CreateAccount = true,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetUserData = true,
                    GetPlayerProfile = true,
                },
            };

            var completionSource = new UniTaskCompletionSource<LoginResult>();
            PlayFabClientAPI.LoginWithCustomID(
                request,
                result => completionSource.TrySetResult(result),
                error => throw new RetryException(ExceptionConfig.FAILED_LOGIN));

            var response = await completionSource.Task;
            if (response == null)
            {
                throw new RetryException(ExceptionConfig.FAILED_LOGIN);
            }

            return response;
        }

        public async UniTask<bool> UpdateUserNameAsync(string name, CancellationToken token)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new RetryException(ExceptionConfig.UNMATCHED_USER_NAME_RULE);
            }

            if (name.Length.IsBetween(3, 10) == false)
            {
                throw new RetryException(ExceptionConfig.UNMATCHED_USER_NAME_RULE);
            }

            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = name,
            };

            var completionSource = new UniTaskCompletionSource<UpdateUserTitleDisplayNameResult>();
            PlayFabClientAPI.UpdateUserTitleDisplayName(
                request,
                result => completionSource.TrySetResult(result),
                error =>
                {
                    // 名前更新失敗の要因を2つに絞る
                    // すでに登録されているユーザー名 or それ以外
                    var message = error.Error == PlayFabErrorCode.NameNotAvailable
                        ? ExceptionConfig.UNMATCHED_USER_NAME_RULE
                        : ExceptionConfig.FAILED_UPDATE_DATA;
                    throw new RetryException(message);
                });

            var response = await completionSource.Task;
            if (response == null)
            {
                throw new RetryException(ExceptionConfig.FAILED_UPDATE_DATA);
            }

            return true;
        }

        public UserData FetchUserData(LoginResult response)
        {
            if (response == null)
            {
                throw new RebootException(ExceptionConfig.NOT_FOUND_DATA);
            }

            var payload = response.InfoResultPayload;
            if (payload == null)
            {
                throw new RebootException(ExceptionConfig.NOT_FOUND_DATA);
            }

            var userDataRecord = payload.UserData;
            if (userDataRecord == null)
            {
                throw new RebootException(ExceptionConfig.NOT_FOUND_DATA);
            }

            var profile = payload.PlayerProfile;
            var userName = profile == null ? "" : profile.DisplayName;
            var userId = profile == null ? "" : profile.PlayerId;

            return new UserData(userName, userId, userDataRecord);
        }

        public async UniTask UpdatePlayRecordAsync(Data.Entity.UserPlayEntity playEntity, CancellationToken token)
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { PlayFabConfig.USER_PLAY_RECORD_KEY, playEntity.ToJson() },
                },
            };

            var completionSource = new UniTaskCompletionSource<UpdateUserDataResult>();
            PlayFabClientAPI.UpdateUserData(
                request,
                result => completionSource.TrySetResult(result),
                error => throw new RetryException(ExceptionConfig.FAILED_UPDATE_DATA));

            var response = await completionSource.Task;
            if (response == null)
            {
                throw new RetryException(ExceptionConfig.FAILED_UPDATE_DATA);
            }
        }
    }
}