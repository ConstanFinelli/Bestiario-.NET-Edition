namespace Blazor.Server.Models
{
    public sealed class SelectOption<TValue>
    {
        public TValue Value { get; set; } = default!;
        public string Text { get; set; } = string.Empty;

        public SelectOption() { }

        public SelectOption(TValue value, string text)
        {
            Value = value;
            Text = text;
        }
    }
}
