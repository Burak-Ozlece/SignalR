using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRServerExample.Hubs
{
	public class MessageHub : Hub
	{
		//public async Task SendMessageAsync(string message, IEnumerable<string> connectionId)
		//public async Task SendMessageAsync(string message, string groupName, IEnumerable<string> connectionIds)
		//public async Task SendMessageAsync(string message, IEnumerable<string> groups)
		public async Task SendMessageAsync(string message, string groupName)
		{

			#region Caller

			// Sadece server'a bildirim gönderen client ile iletişim kurar.

			//await Clients.Caller.SendAsync("receiveMessage", message);

			#endregion

			#region All
			//Server'a bağlı olan tüm clientlarla iletişim kurar

			//await Clients.All.SendAsync("receiveMessage", message);
			#endregion

			#region Others
			// Sadece server'a bildirim gönderen client dışında Server'a bağlı olan tüm client'lara mesaj gönderir.

			//await Clients.Others.SendAsync("receiveMessage", message);
			#endregion

			#region Hub Clients Method

			#region AllExcept
			// Belirtilen clientlara hariç server'a bağlı olan tüm clientlara bildiride bulunur.
			//await Clients.AllExcept(connectionId).SendAsync("receiveMessage", message);
			#endregion

			#region Client
			// Server'a bağlı olan clientlar arasında sadece belirli bir clienta bildiride bulunur.
			//await Clients.Client(connectionId.First()).SendAsync("receiveMessage", message);
			#endregion

			#region Clients
			//await Clients.Clients(connectionId).SendAsync("receiveMessage", message);
			#endregion

			#region Group
			//Belirtilen grupdaki tüm clientlara bildiride bulunur.
			//Önce gruplar oluşturulmalı ve ardından clientlar gruplara üye olmalı.
			//await Clients.Group(groupName).SendAsync("receiveMessage", message);
			#endregion

			#region GroupExcept
			//Belirtilen grupdaki, belirtilen gruplar dışındaki tüm clientlara mesaj iletmemizi sağlayan bir fonksiyondur.
			//await Clients.GroupExcept(groupName, connectionIds).SendAsync("receiveMessage", message);
			#endregion

			#region Groups
			//Birden çok grupdaki clientlara bildiride bulunmamızı sağlayan fonskiyondur
			//await Clients.Groups(groups).SendAsync("receiveMessage", message);
			#endregion
			#region OtherInGroup
			await Clients.OthersInGroup(groupName).SendAsync("receiveMessage", message);
			#endregion

			#endregion

		}

		public override async Task OnConnectedAsync()
		{
			await Clients.Caller.SendAsync("getConnectionId", Context.ConnectionId);
		}

		public async Task addGroup(string connectionId, string groupName)
		{
			await Groups.AddToGroupAsync(connectionId, groupName);
		}
	}
}
