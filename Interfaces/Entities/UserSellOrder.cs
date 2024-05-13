using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class UserSellOrder
	{
		public Guid? UserId { get; set; }	
		public User? User { get; set; }

		public Guid? SellOrderId { get; set; }
		public SellOrder? SellOrder { get; set; }
	}
}
