﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace OOpro.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OOproDB : DbContext
    {
        public OOproDB()
            : base("name=OOproDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ItemRev> ItemRev { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Save> Save { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
