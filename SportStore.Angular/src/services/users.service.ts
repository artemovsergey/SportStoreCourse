import User from "../models/user";

function getLocalUsers(): User[] {
    var users = [{"id":"1","name":"user1"}, {"id":"2","name":"user2"}, {"id":"3","name":"user2"}]
    return users;
}

export default getLocalUsers()
