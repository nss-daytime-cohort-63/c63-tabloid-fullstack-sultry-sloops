import React from "react";
import { Link } from "react-router-dom";

const Tag = ({ t }) => {
    return (
        <li>{t.name}</li>
    );
}
export default Tag;