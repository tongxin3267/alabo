using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Repositories;
using ZKCloud.Domain.Services;
using ZKCloud.Web.Apps.Admin.src.Entities;

namespace ZKCloud.Web.Apps.Admin.src.Domains.Services {
	public class AdminService : IAdminService {
		public void AddSingle(Entities.Admin admin) {
			throw new NotImplementedException();
		}

		public void AssignToExistUser(string username, bool superAdmin, long roleId) {
			throw new NotImplementedException();
		}

		public void Delete(params int[] ids) {
			throw new NotImplementedException();
		}

		public IList<Entities.Admin> GetList() {
			throw new NotImplementedException();
		}

		public Entities.Admin Read(long id) {
			throw new NotImplementedException();
		}

		public IList<Entities.Admin> ReadMany() {
			throw new NotImplementedException();
		}

		public T Repository<T>() where T : IRepository {
			throw new NotImplementedException();
		}

		public T Service<T>() where T : IService {
			throw new NotImplementedException();
		}

		public void UpdateSingle(Entities.Admin admin) {
			throw new NotImplementedException();
		}
	}
}
		

	
	
