using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class VerificationCode : IEntity
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public int Code { get; set; }
    }
}
