using System;

namespace InternetForum.Domain.Enums
{
    [Flags]
    public enum CommunicationChannelPermission
    {
        Admin = 0,

        Post = 2,

        Invite = 4,

        ManageUsers = 8
    }
}
