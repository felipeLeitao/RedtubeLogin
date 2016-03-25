using System;

namespace NetCoders.Redtube.WebApi.Models
{
    public class User
    {
        public Int32 Codigo { get; set; }

        public String Usuario { get; set; }

        public String UrlAvatar { get; set; }

        public Boolean isAtivo { get; set; }
    }
}