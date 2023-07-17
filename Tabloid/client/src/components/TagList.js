import React from "react";
import { useState, useEffect } from "react";
import { getAllTags, updateTag } from "../modules/tagManager";
import Tag from "./Tag";
import TagMaker from "./TagMaker";
import { Modal, ModalHeader, ModalBody, ModalFooter, Button, Form, Input } from "reactstrap";
import { deleteTag } from "../modules/tagManager";


const TagList = () => {
    const [tags, setTags] = useState([]);
    const [show, setShow] = useState(false);
    const [selTag, setSelTag] = useState({
        id: 0,
        name: ""
    })

    const [showUpdate, setShowUpdate] = useState(false);
    const toggleUpdate = () => { setShowUpdate(!showUpdate) };

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

    const yesUpdate = (event) => {
        event.preventDefault();
        updateTag(selTag)
            .then(toggleUpdate())
            .then(getTags());
    }

    return <>
        <TagMaker getTags={getTags} />
        <div>Tag List</div>
        <ul>
            {tags.map((t) => {
                return (<>
                    <Tag tag={t} key={t.id} toggle={toggle} setSelTag={setSelTag} toggleUpdate={toggleUpdate} />
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
        <Modal isOpen={showUpdate} toggle={toggleUpdate}>
            <ModalHeader closeButton>Update Tag Confirmation</ModalHeader>
            <ModalBody>Are you sure that you'd like to update {selTag.id}, {selTag.name}?</ModalBody>
            <Form>
                <Input
                    required autofocus
                    type="text"
                    value={selTag.name}
                    onChange={
                        (event) => {
                            const copy = { ...selTag }
                            copy.name = event.target.value
                            setSelTag(copy)
                        }
                    } />
            </Form>
            <ModalFooter>
                <Button variant="secondary" onClick={toggleUpdate}>
                    Cancel
                </Button>
                <Button variant="primary" onClick={event => yesUpdate(event)}>
                    Confirm Update
                </Button>
            </ModalFooter>
        </Modal>
    </>
}
export default TagList;