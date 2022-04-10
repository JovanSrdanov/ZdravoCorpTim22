namespace Model
{
    public class Address
    {
        public string street;
        public string number;
        public string city;
        public string country;

        public System.Collections.Generic.List<User> user;

        public System.Collections.Generic.List<User> User
        {
            get
            {
                if (user == null)
                    user = new System.Collections.Generic.List<User>();
                return user;
            }
            set
            {
                RemoveAllUser();
                if (value != null)
                {
                    foreach (User oUser in value)
                        AddUser(oUser);
                }
            }
        }


        public void AddUser(User newUser)
        {
            if (newUser == null)
                return;
            if (this.user == null)
                this.user = new System.Collections.Generic.List<User>();
            if (!this.user.Contains(newUser))
            {
                this.user.Add(newUser);
            }
        }


        public void RemoveUser(User oldUser)
        {
            if (oldUser == null)
                return;
            if (this.user != null)
                if (this.user.Contains(oldUser))
                {
                    this.user.Remove(oldUser);
                }
        }


        public void RemoveAllUser()
        {
            if (user != null)
            {
                System.Collections.ArrayList tmpUser = new System.Collections.ArrayList();
                foreach (User oldUser in user)
                    tmpUser.Add(oldUser);
                user.Clear();
                tmpUser.Clear();
            }
        }

    }
}