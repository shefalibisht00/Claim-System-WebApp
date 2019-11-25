using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ClaimSystem.BLL.Mappings;
using ClaimSystem.DAL;
using ClaimSystem.DAL.Entities;
using ClaimSystem.shared.dto;

namespace ClaimSystem.BLL
{
    public class UserService 
    {
        private IUserRepository userRepo;

        public UserService(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        public Task<int> InsertUser(ApplicationUserDto dto)
        {
            try
            {
                var entity = AutoMapperConfiguration.Mapper.Map<ApplicationUser>(dto);
                return userRepo.Insert(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:UsersBusiness::InsertUser::Error occured.", ex);
            }
        }

        // Get all user details
        public IEnumerable<ApplicationUserDto> GetAllUsers()
        {
            Console.WriteLine("hello");
            try
            {

                Debug.WriteLine("hello");
                var result = userRepo.SelectAll();
                Debug.WriteLine("\n\n\n list \n\n"+result);
                var dto = AutoMapperConfiguration.Mapper.Map<IEnumerable<ApplicationUserDto>>(result);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:UsersBusiness::GetAllUsers::Error occured.", ex);
            }
        }

        // Get particular user details
        public ApplicationUserDto GetUserById(int id)
        {
            try
            {
                var result = userRepo.SelectById(id);
                var dto = AutoMapperConfiguration.Mapper.Map<ApplicationUserDto>(result);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:UsersBusiness::GetUserById::Error occured.", ex);
            }
        }
        public bool UserEmailExist(string email)
        {
            try
            {
                var result = userRepo.SelectUserByEmail(email);
                if (result == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:UsersBusiness::GetUserByEmail::Error occured.", ex);
            }
        }


    }
}
