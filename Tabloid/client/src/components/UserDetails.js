import React, { useEffect, useState } from "react";
import { getUserById } from "../modules/userManager";
import { useParams } from "react-router-dom";
export const UserDetails =()=>{
    const [user,setUser] = useState();
    const { id } = useParams();

    useEffect(()=>{
        getUserById(id).then(setUser)
    },[])

    if(!user){
        return null;
    }

    const date = new Date(user.createDateTime)
    return <div>
        <h3>User Details</h3>
       <section>{user.fullName}</section>
       
       {
        user.imageLocation === null ?  <section><img height="200px"src="https://www.disneyclips.com/images/images/goofy_thinking.gif"></img></section> :  <section><img height="200px"src={user.imageLocation}></img></section>
       }      

       <section>Display Name: {user.displayName}</section>
       <section>Email: {user.email}</section>
       <section>Creation Date: {date.getMonth() + 1}/{date.getDate()}/{date.getFullYear()}</section>
       <section>User Type: {user.userType.name}</section>
       
    </div>
}