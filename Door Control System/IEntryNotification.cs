namespace Door_Control_System
{
    public interface IEntryNotification
    {
        void NotifyEntryGranted();
        void NotifyEntryDenied();
    }
}