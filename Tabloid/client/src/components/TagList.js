import React from "react";
import { useState, useEffect } from "react";
import { getAllTags } from "../modules/tagManager";
import Tag from "./Tag";
import TagMaker from "./TagMaker";
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from "reactstrap";
import { deleteTag } from "../modules/tagManager";


const TagList = () => {
    const [tags, setTags] = useState([]);
    const [show, setShow] = useState(false);
    const [selTag, setSelTag] = useState({
        id: 0,
        name: ""
    })

    const toggle = () => { setShow(!show) };

    const getTags = () => {
        getAllTags().then(data => setTags(data));
    }
    useEffect(
        () => {
            getTags();
        }, []
    );

    const yesDelete = (event) => {
        event.preventDefault();
        deleteTag(selTag.id)
            .then(toggle())
            .then(getTags());
    };

    return <>
        <TagMaker getTags={getTags} />
        <div>Tag List</div>
        <ul>
            {tags.map((t) => {
                return (<>
                    <Tag tag={t} key={t.id} toggle={toggle} setSelTag={setSelTag} />
                </>
                )
            })}
        </ul>
        <Modal isOpen={show} toggle={toggle}>
            <ModalHeader closeButton>Delete Tag Confirmation</ModalHeader>
            <ModalBody>This will permanently delete the tag {selTag.id}, {selTag.name}. Confirmation is required.</ModalBody>
            <ModalFooter>
                <Button variant="secondary" onClick={toggle}>
                    Cancel
                </Button>
                <Button variant="primary" onClick={event => yesDelete(event)}>
                    Confirm Delete
                </Button>
            </ModalFooter>
        </Modal>
    </>
}
export default TagList;