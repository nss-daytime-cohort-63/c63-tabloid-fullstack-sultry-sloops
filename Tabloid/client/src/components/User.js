import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { deactivateUser } from "../modules/userManager";
export const User = ({ user }) =>{
const navigate = useNavigate()
  return (
<Card >


        <section><Link to={`/users/${user.id}`}>Name: {user.fullName}</Link></section>
        <section>Display Name: {user.displayName}</section>
        <section>Type: {user.userType.name}</section>
        <section><button onClick={()=>navigate(`/users/edit/${user.id}`)}>Edit</button></section>
       


</Card>
    );
}