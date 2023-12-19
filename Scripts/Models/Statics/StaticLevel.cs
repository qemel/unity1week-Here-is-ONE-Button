using UniRx;

namespace App.Scripts.Models.Statics
{
    public static class StaticLevel
    {
        public static IReadOnlyReactiveProperty<int> CurrentLevel => _currentLevel;
        private static readonly ReactiveProperty<int> _currentLevel = new(0);

        public static void NextLevel()
        {
            _currentLevel.Value += 1;
        }
        
        public static void SetLevel(int level)
        {
            _currentLevel.Value = level;
        }
    }
}