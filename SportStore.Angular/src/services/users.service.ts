import User from "../models/user";

function getLocalUsers(): User[] {
    var users = [{"id":"1","name":"user1","login":"login"},
                 {"id":"2","name":"user2","login":"login"},
                 {"id":"3","name":"user2","login":"login"}]
    return users;
}

export default getLocalUsers()
