import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";

export const User = ({ user }) =>{

    return (
<Card >


        <section><Link to={`/users/${user.id}`}>Name: {user.fullName}</Link></section>
        <section>Display Name: {user.displayName}</section>
        <section>Type: {user.userType.name}</section>
   


</Card>
    );
}