//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkarplineChat.Model.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserChat
    {
        public System.Guid ChatId { get; set; }
        public System.Guid MessageFrom { get; set; }
        public string Message { get; set; }
        public System.DateTime MessageDateTime { get; set; }
    
        public virtual User User { get; set; }
    }
}
