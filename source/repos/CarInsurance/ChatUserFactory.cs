using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInsurance
{
    public static class ChatUserFactory
    {
        private static readonly Dictionary<long, ChatUser> _users = new();

        public static ChatUser GetOrCreate(long chatId) { 
            if(!_users.TryGetValue(chatId, out var user))
            {
                user = new ChatUser(chatId);
                _users[chatId] = user;
            }
            return user;
        }
    }
}
