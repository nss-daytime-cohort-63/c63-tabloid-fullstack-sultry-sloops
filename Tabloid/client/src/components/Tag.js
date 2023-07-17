import React from "react";
import { Button } from "reactstrap";

const Tag = ({ tag, toggle, setSelTag, toggleUpdate }) => {

    const extraToggle = () => {
        setSelTag(tag);
        toggle();
    };

    const updateToggle = () => {
        setSelTag(tag);
        toggleUpdate();
    };

    return (
        <>
            <li>{tag.name}</li>
            <Button variant="primary" onClick={extraToggle}>
                Delete
            </Button>
            <Button variant="secondary" onClick={updateToggle}>
                Update
            </Button>
        </>
    );
}

export default Tag;