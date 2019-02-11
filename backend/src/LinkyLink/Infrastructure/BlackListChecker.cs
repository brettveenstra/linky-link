using System;
using System.Linq;
using System.Threading.Tasks;

namespace LinkyLink.Infrastructure
{
    public interface IBlackListChecker
    {
        Task<bool> Check(string value);
    }

    public class EnvironmentBlackListChecker : IBlackListChecker
    {
        private string[] _blackList;

        public EnvironmentBlackListChecker(string key)
        {
            string settingsValue = Environment.GetEnvironmentVariable(key);
            this._blackList = settingsValue != null ? settingsValue.Split(',') : new string[] { };
        }

        public Task<bool> Check(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));

            return Task.FromResult(_blackList.Any()? _blackList.Contains(value): true);
        }
    }
}