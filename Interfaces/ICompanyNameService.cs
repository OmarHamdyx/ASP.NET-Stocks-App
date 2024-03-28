using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	public interface ICompanyNameService
	{
		public Task<CompanyInfo?> GetCompanyInfoAsync(string? symbol);

	}
}
