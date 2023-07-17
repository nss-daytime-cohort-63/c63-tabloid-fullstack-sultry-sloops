import React from "react";
import { Button } from "reactstrap";

const Tag = ({ tag, toggle, setSelTag }) => {

    const extraToggle = () => {
        setSelTag(tag);
        toggle();
    }

    return (
        <>
            <li>{tag.name}</li>
            <Button variant="primary" onClick={extraToggle}>
                Delete
            </Button>
        </>
    );
}

export default Tag;