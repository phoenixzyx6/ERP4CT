using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZLERP.Model.ViewModels
{
    public class TreeNode
    {
        public string name { get; set; }
        public string title { get; set; }
        public virtual List<TreeNode> children { get; set; }
        public bool open { get; set; }
        public bool isParent { get; set; }
        public string icon { get; set; }
        public string font { get; set; }
        //public string click { get; set; }
        public string dbId {get;set;}
        public string termId { get; set; }
        public string lonlat { get; set; }
        public string otherinfo { get; set; }
        public bool nocheck { get; set; }

        public TreeNode()
        {

        }
        public TreeNode(string name, string title, List<TreeNode> children, bool open, bool isParent, string icon, string font, string dbId, string termId, string lonlat, string otherinfo,bool _nocheck)
        {
            this.name = name;
            this.title = title;
            this.open = open; this.isParent = isParent; this.children = children;
            if (!string.IsNullOrWhiteSpace(icon))
            {
                this.icon = icon;
            }
            if (!string.IsNullOrWhiteSpace(dbId))
            {
                this.dbId = dbId;
            }
            if (!string.IsNullOrWhiteSpace(termId))
            {
                this.termId = termId;
            }
            if (!string.IsNullOrWhiteSpace(lonlat))
            {
                this.lonlat = lonlat;
            }
            if (!string.IsNullOrWhiteSpace(font))
            {
                this.font = font;
            }
            if (!string.IsNullOrWhiteSpace(otherinfo))
            {
                this.otherinfo = otherinfo;
            }
            nocheck = _nocheck;
        }

        public TreeNode(string name, string title, List<TreeNode> children, bool open, bool isParent, string icon,string font,string dbId,string termId,string lonlat,string otherinfo)
        {
            this.name = name; 
            this.title = title; 
            this.open = open; this.isParent = isParent; this.children = children;
            if (!string.IsNullOrWhiteSpace(icon))
            {
                this.icon = icon;
            }
            if (!string.IsNullOrWhiteSpace(dbId))
            {
                this.dbId = dbId;
            }
            if (!string.IsNullOrWhiteSpace(termId))
            {
                this.termId = termId;
            }
            if (!string.IsNullOrWhiteSpace(lonlat))
            {
                this.lonlat = lonlat;
            }
            if (!string.IsNullOrWhiteSpace(font))
            {
                this.font = font;
            }
            if (!string.IsNullOrWhiteSpace(otherinfo))
            {
                this.otherinfo = otherinfo;
            }
        }
    }
}