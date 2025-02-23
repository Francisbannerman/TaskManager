namespace TaskManager.Client.AuthService;

public class AppState
{
    public bool IsLoggedIn { get; private set; }

    public event Action? OnChange;

    public void SetLoginState(bool isLoggedIn)
    {
        IsLoggedIn = isLoggedIn;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}