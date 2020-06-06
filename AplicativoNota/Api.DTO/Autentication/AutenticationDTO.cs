using System;
using System.Collections.Generic;
using System.Text;

namespace Api.DTO
{
    public class AutenticaoRequest
    {
        public string UserName{ get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long Matricula { get; set; }
    }
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
