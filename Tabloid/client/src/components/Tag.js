import React from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from "reactstrap";
import { deleteTag } from "../modules/tagManager";

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