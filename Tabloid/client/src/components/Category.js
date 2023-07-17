import React from "react";
import { Button } from "reactstrap";

const Category = ({ c }) => {
    return (
        <>
            <li>{c.name}</li>
            <Button>Delete</Button>
        </>
    );
}





export default Category;