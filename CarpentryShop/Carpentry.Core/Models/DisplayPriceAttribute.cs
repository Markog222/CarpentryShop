using System;

namespace Carpentry.Core.Models
{
    internal class DisplayPriceAttribute : Attribute
    {
        private string v;

        public DisplayPriceAttribute(string v)
        {
            this.v = v;
        }
    }
}