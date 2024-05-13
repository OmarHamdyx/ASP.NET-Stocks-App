using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class UserBuyOrder
	{
		public Guid? UserId { get; set; }	
		public User? User { get; set; }

		public Guid? BuyOrderId { get; set; }
		public BuyOrder? BuyOrder { get; set; }
	}
}
