import React from "react";
import { Link } from "react-router-dom";

const Category = ({ c }) => {
    return (
        <li>{c.name}</li>
    );
}
export default Category;