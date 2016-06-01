using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetRx.Entities;
using JetRx.Common.Data.SqlServer;
using System.Net.Http;
using System.Web;
using System.Net;
using System.Web.Security;

namespace JetRx.Common.Services
{
    public class AuthorizationService
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public  string GenerateAccessToken(int appId, int userId, int deviceId, double lifetime = 30)
        {
            using(var context = new JetRxContext())
            {
                AccessToken token = new AccessToken();
                token.ClientId = appId;
                token.UserId = userId;
                token.Secret = GenerateCode();
                token.IssuedUtc = DateTime.UtcNow;
                token.ExpiresUtc = DateTime.UtcNow.AddMinutes(lifetime);

                context.AccessTokens.Add(token);
                context.SaveChanges();
                return token.Secret;
                    
            }
           
        }

        public  device RegisterDevice(int appId, device Device)
        {
            using (var context = new JetRxContext())
            {
                device registeredDevice = new device(GenerateCode());
                var existingDevice = context.Devices.FirstOrDefault(d => d.deviceidentifier == Device.deviceidentifier || d.phonenumber ==  Device.phonenumber);

                if (existingDevice != null)
                {
                    registeredDevice.id = existingDevice.id;
                    registeredDevice.deviceidentifier = Device.deviceidentifier;
                    registeredDevice.devicename = Device.devicename;
                    registeredDevice.devicetype = Device.devicetype;
                    registeredDevice.phonenumber = Device.phonenumber;
                  
                }
                else
                {
                    registeredDevice.deviceidentifier = Device.deviceidentifier;
                    registeredDevice.devicename = Device.devicename;
                    registeredDevice.devicetype = Device.devicetype;
                    registeredDevice.phonenumber = Device.phonenumber;
                    context.Devices.Add(registeredDevice);
                }   
               context.SaveChanges();

                user tempUser = new user();
                tempUser.password = Membership.GeneratePassword(10, 3);
                tempUser.phone_number = registeredDevice.phonenumber;
                registeredDevice.device_user = RegisterUser(appId, registeredDevice.id, tempUser);


                return registeredDevice;
            }
        }


        public  user LoginUser(int appId, int deviceId, string password, string email, string phoneNumber)
        {
            user User = new user();
            using (var context = new JetRxContext())
            {
               
                var Customer = context.Customers.FirstOrDefault(c => c.email == email);
                
                if ((Customer == null) && (!CryptoService.VerifyHash(password, Customer.password)))
                    throw new Exception("Unauthorized User");
                   
                User.email = email;
                User.password = password;
                User.accesstoken = GenerateAccessToken(appId, Customer.id, deviceId);
                User.phone_number = Customer.phone_number;

            }

            return User;
        }

        public user RegisterUser(int appId, int deviceId, user User)
        {
            using (var context = new JetRxContext())
            {
                var device = context.Devices.FirstOrDefault(d => d.id == deviceId);

                if (device == null)
                    throw new Exception("Unauthorized Device");


                var Customer = new customer
                {
                    email = User.email,
                    phone_number = User.phone_number,
                    password = CryptoService.ComputeHash(User.password, null)

                };

                context.Customers.Add(Customer);

                context.CustomerDevices.Add(new CustomerDevice
                {
                    DeviceId = device.id,
                    CustomerId = Customer.id
                    
                });

                context.SaveChanges();

                User.accesstoken = GenerateAccessToken(appId, Customer.id, device.id);
                User.phone_number = device.phonenumber;
                return User;
            }
        }

        public static JetRxSession AuthenticateRequest(string Appkey, string DeviceKey, string AccessKey,bool MustHaveAccessKey, bool MustHaveDeviceKey)
        {
          
            JetRxSession userSession = new JetRxSession();
             
            using (var context = new JetRxContext())
            {
                try {


                    var app = context.Clients.FirstOrDefault(a => a.Secret == Appkey);

                    if (app == null)
                        throw new Exception("Unauthorized Application");

                    userSession.AppId = app.Id;

                    var device = context.Devices.FirstOrDefault(d => d.appdeviceKey == DeviceKey);

                    if (MustHaveDeviceKey && (string.IsNullOrEmpty(DeviceKey)) && device == null)
                        throw new Exception("Unauthorized Device");
                    else
                    {
                        if((!string.IsNullOrEmpty(DeviceKey)) && device != null)
                            userSession.Device = device;
                    }

                  

                    var accessToken = context.AccessTokens.FirstOrDefault(a => a.Secret == AccessKey);

                    if (MustHaveAccessKey && accessToken == null)
                        throw new Exception("Unauthorized User");
                    else
                    {
                        if (accessToken !=null)
                        {
                            if(accessToken.ExpiresUtc.GetValueOrDefault() < DateTime.UtcNow)
                                throw new Exception("Token has Expired");

                            userSession.Customer = context.Customers.FirstOrDefault(a=>a.id == accessToken.UserId);
                            accessToken.ExpiresUtc = DateTime.UtcNow.AddMinutes(30);
                            context.SaveChanges();
                        }
                    }

                   
                    
                  

                }
                catch(Exception e)
                {
                    throw e;
                }

                return userSession;

            }

        }

         string GenerateCode()
        {
            return Guid.NewGuid().ToString() + Guid.NewGuid().ToString() + "-" + DateTime.UtcNow.Ticks.ToString();
        }
    }
}
