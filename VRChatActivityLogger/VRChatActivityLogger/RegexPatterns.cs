﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace VRChatActivityLogger
{
    /// <summary>
    /// 正規表現のグループ名定義
    /// </summary>
    static public class PatternType
    {
        public static readonly string ReceivedInvite = "ReceivedInvite";
        public static readonly string ReceivedRequestInvite = "ReceivedRequestInvite";
        public static readonly string SendInvite = "SendInvite";
        public static readonly string SendRequestInvite = "SendRequestInvite";
        public static readonly string JoinedRoom1 = "JoinedRoom1";
        public static readonly string JoinedRoom2 = "JoinedRoom2";
        public static readonly string MetPlayer = "MetPlayer";
        public static readonly string LeavePlayer = "LeavePlayer";
        public static readonly string SendFriendRequest = "SendFriendRequest";
        public static readonly string ReceivedFriendRequest = "ReceivedFriendRequest";
        public static readonly string AcceptFriendRequest = "AcceptFriendRequest";
        public static readonly string ReceivedInviteResponse = "ReceivedInviteResponse";
        public static readonly string ReceivedRequestInviteResponse = "ReceivedRequestInviteResponse";
    }

    /// <summary>
    /// 正規表現を定義するクラス
    /// </summary>
    public static class RegexPatterns
    {
        public static Regex ReceivedInviteDetail { get; }
        public static Regex ReceivedRequestInviteDetail { get; }
        public static Regex SendInviteDetail { get; }
        public static Regex SendRequestInviteDetail { get; }
        public static Regex JoinedRoom1Detail { get; }
        public static Regex JoinedRoom2Detail { get; }
        public static Regex MetPlayerDetail { get; }
        public static Regex LeavePlayerDetail { get; }
        public static Regex SendFriendRequestDetail { get; }
        public static Regex ReceivedFriendRequestDetail { get; }
        public static Regex AcceptFriendRequestDetail { get; }
        public static Regex ReceivedInviteResponseDetail { get; }
        public static Regex ReceivedRequestInviteResponseDetail { get; }
        public static Regex All { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static RegexPatterns()
        {
            //ログの種類判別(個別)
            string header = @"^\d{4}\.\d{2}\.\d{2}\s\d{2}:\d{2}:\d{2}\sLog\s{8}-\s{2}";

            string receivedInvite = header + @"Received Notification:.+type:invite,.+$";
            string receivedRequestInvite = header + @"Received Notification:.+type:requestInvite,.+$";
            string sendInvite = header + @"Send notification:.+type:invite,.+$";
            string sendRequestInvite = header + @"Send notification:.+type:requestInvite,.+$";
            string joinedRoom1 = header + @"\[(RoomManager|[Ǆǅ]*|Behaviour)\] Joining w.+$";
            string joinedRoom2 = header + @"\[(RoomManager|[Ǆǅ]*|Behaviour)\] Joining or Creating Room:.+$";
            string metPlayer = header + @"\[(Player|[Ǆǅ]*|Behaviour)\] Initialized PlayerAPI.+$";
            string leavePlayer = header + @"\[(Player|[Ǆǅ]*|Behaviour)\] Unregistering .+$";
            string sendFriendRequest = header + @"Send notification:.+type:friendRequest,.+$";
            string receivedFriendRequest = header + @"Received Notification:.+type:friendRequest,.+$";
            string acceptFriendRequest = header + @"AcceptFriendRequest.+$";
            string receivedInviteResponse = header + @"Received Notification:.+type:inviteResponse,.+$";
            string receivedRequestInviteResponse = header + @"Received Notification:.+type:requestInviteResponse,.+$";


            //ログの種類判別(一括)
            string all = "";
            all += $@"(?<ReceivedInvite>{receivedInvite})|";
            all += $@"(?<ReceivedRequestInvite>{receivedRequestInvite})|";
            all += $@"(?<SendInvite>{sendInvite})|";
            all += $@"(?<SendRequestInvite>{sendRequestInvite})|";
            all += $@"(?<JoinedRoom1>{joinedRoom1})|";
            all += $@"(?<JoinedRoom2>{joinedRoom2})|";
            all += $@"(?<MetPlayer>{metPlayer})|";
            all += $@"(?<LeavePlayer>{leavePlayer})|";
            all += $@"(?<SendFriendRequest>{sendFriendRequest})|";
            all += $@"(?<ReceivedFriendRequest>{receivedFriendRequest})|";
            all += $@"(?<AcceptFriendRequest>{acceptFriendRequest})|";
            all += $@"(?<ReceivedInviteResponse>{receivedInviteResponse})|";
            all += $@"(?<ReceivedRequestInviteResponse>{receivedRequestInviteResponse})";
            All = new Regex(all, RegexOptions.Compiled);

            //ログの詳細を解析
            string detailHeader = @"^(\d{4}\.\d{2}\.\d{2}\s\d{2}:\d{2}:\d{2})\sLog\s{8}-\s{2}";

            string receivedInviteDetail = detailHeader + @"Received Notification: <Notification from username:(.+), sender user id:(.{40}).+ of type: invite, id: (.{40}).+worldId=(.+), worldName=(.+?)(, inviteMessage=(.+?))?(, imageUrl=(.+?))?}}, type:invite,.+$";
            string receivedRequestInviteDetail = detailHeader + @"Received Notification: <Notification from username:(.+), sender user id:(.{40}).+ of type: requestInvite, id: (.{40}),.+{{(requestMessage=(.+?))?,? ?(imageUrl=(.+?))??}}, type:requestInvite,.+$";
            string sendInviteDetail = detailHeader + @"Send notification:.+sender user.+ to (.{40}).+worldId=([^,]+),.+worldName=(.+?)(, messageSlot=.+)?}}, type:invite,.+message: ""(.+)?"".+$";
            string sendRequestInviteDetail = detailHeader + @"Send notification:.+sender user.+ to (.{40}).+type:requestInvite,.+message: ""(.+)?"".+$";
            string metPlayerDetail = detailHeader + @"\[(Player|[Ǆǅ]*|Behaviour)\] Initialized PlayerAPI ""(.*)"" is (remote|local)$";
            string leavePlayerDetail = detailHeader + @"\[(Player|[Ǆǅ]*|Behaviour)\] Unregistering (.*)$";
            string joinedRoom1Detail = detailHeader + @"\[(RoomManager|[Ǆǅ]*|Behaviour)\] Joining (.+)$";
            string joinedRoom2Detail = detailHeader + @"\[(RoomManager|[Ǆǅ]*|Behaviour)\] Joining or Creating Room: (.+)$";
            string sendFriendRequestDetail = detailHeader + @"Send notification:.+sender user.+ to (.{40}).+type:friendRequest,.+$";
            string receivedFriendRequestDetail = detailHeader + @"Received Notification: <Notification from username:(.+), sender user id:(.{40}).+ of type: friendRequest, id: (.{40}),.+type:friendRequest,.+$";
            string acceptFriendRequestDetail = detailHeader + @"AcceptFriendRequest Notification:<Notification from username:(.+), sender user id:(.{40}).+ of type: friendRequest, id: (.{40}),.+type:friendRequest,.+$";
            string receivedInviteResponseDetail = detailHeader + @"Received Notification: <Notification from username:(.+), sender user id:(.{40}).+ of type: inviteResponse, id: (.{40}).+{{.+?(, responseMessage=(.+?))?(, imageUrl=(.+?))?}}, type:inviteResponse,.+$";
            string receivedRequestInviteResponseDetail = detailHeader + @"Received Notification: <Notification from username:(.+), sender user id:(.{40}).+ of type: requestInviteResponse, id: (.{40}).+{{.+?(responseMessage=(.+?))?(, imageUrl=(.+?))?}}, type:requestInviteResponse,.+$";

            ReceivedInviteDetail = new Regex(receivedInviteDetail, RegexOptions.Compiled);
            ReceivedRequestInviteDetail = new Regex(receivedRequestInviteDetail, RegexOptions.Compiled);
            SendInviteDetail = new Regex(sendInviteDetail, RegexOptions.Compiled);
            SendRequestInviteDetail = new Regex(sendRequestInviteDetail, RegexOptions.Compiled);
            MetPlayerDetail = new Regex(metPlayerDetail, RegexOptions.Compiled);
            LeavePlayerDetail = new Regex(leavePlayerDetail, RegexOptions.Compiled);
            JoinedRoom1Detail = new Regex(joinedRoom1Detail, RegexOptions.Compiled);
            JoinedRoom2Detail = new Regex(joinedRoom2Detail, RegexOptions.Compiled);
            SendFriendRequestDetail = new Regex(sendFriendRequestDetail, RegexOptions.Compiled);
            ReceivedFriendRequestDetail = new Regex(receivedFriendRequestDetail, RegexOptions.Compiled);
            AcceptFriendRequestDetail = new Regex(acceptFriendRequestDetail, RegexOptions.Compiled);
            ReceivedInviteResponseDetail = new Regex(receivedInviteResponseDetail, RegexOptions.Compiled);
            ReceivedRequestInviteResponseDetail = new Regex(receivedRequestInviteResponseDetail, RegexOptions.Compiled);
        }
    }
}
