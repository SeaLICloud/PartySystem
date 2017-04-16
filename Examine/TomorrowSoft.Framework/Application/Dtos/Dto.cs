using System;
using System.Runtime.Serialization;

namespace TomorrowSoft.Framework.Application.Dtos
{
    [DataContract]
    public class Dto
    {
        [DataMember]
        public Guid Id { get; set; }
    }
}