using System;

namespace Atrico.Lib.DomainModel.Tests.Annotations
{
    [AttributeUsage(
        AttributeTargets.Parameter | AttributeTargets.Property |
        AttributeTargets.Field, Inherited = true)]
    public sealed class HtmlElementAttributesAttribute : Attribute
    {
        public HtmlElementAttributesAttribute()
        {
        }

        public HtmlElementAttributesAttribute([NotNull] string name)
        {
            Name = name;
        }

        [NotNull]
        public string Name { get; private set; }
    }
}