using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Door_Control_System
{
    public class DoorControl
    {
        enum DoorState
        {
            DoorClosed, DoorOpening, DoorOpen, DoorBreached
        }

        private IDoor _door;
        private IUserValidation _user;
        private IEntryNotification _noti;
        private IAlarm _alarm;
        private DoorState _state;
        private bool _valid;

        public DoorControl(IDoor door, IUserValidation user, IEntryNotification noti, IAlarm alarm)
        {
            _door = door;
            _user = user;
            _noti = noti;
            _alarm = alarm;
            _state = DoorState.DoorClosed;
        }

        public void RequestEntry(int id)
        {
            _valid = _user.ValidateEntryRequest(id);
            if (_valid)
            {
                _door.Open();
                _noti.NotifyEntryGranted();
            }
            else
            {
                _noti.NotifyEntryDenied();
            }
        }

        public void DoorOpened()
        {
            switch (_state)
            {
                case (DoorState.DoorOpening):
                    _door.Close();
                    _state = DoorState.DoorOpen;
                    break;
                case (DoorState.DoorClosed):
                    _door.Close();
                    _state = DoorState.DoorBreached;
                    _alarm.SignalAlarm();
                    break;
            }
            
        }

        public void DoorClosed()
        {

        }


    }
}
