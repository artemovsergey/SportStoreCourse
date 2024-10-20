import { Injectable } from '@angular/core';
import User from '../models/user'

@Injectable({
  providedIn: 'root'
})
export class UsersLocalService {

  getLocalUsers(): User[] {
    var users = [{ "id": 1, "name": "user1", "login": "user1" },
                 { "id": 2, "name": "user2", "login": "user2" },
                 { "id": 3, "name": "user3", "login": "user3" }]
    return users;

  }

}
