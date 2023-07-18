
import { Button, FormGroup, Input, Label, Modal, ModalBody, ModalFooter, ModalHeader } from "reactstrap";
import "./postDetails.css";
import { useEffect } from "react";
import { getAllCategories } from "../modules/categoryManager";
import { useState } from "react";

export const PostDetailsEditModal = ({ isOpen, toggle, editP, post, editPost }) => {

    const [categories, setCategories] = useState([]);

    const getCategories = () => {
        getAllCategories().then(data => setCategories(data));

    }

    useEffect(
        () => {
            getCategories()
        },
        []
    )

    return (
        <div>
            <Modal isOpen={isOpen} toggle={toggle}>
                <ModalHeader toggle={toggle}>Delete Confirmation</ModalHeader>
                <ModalBody>
                    <FormGroup>
                        <FormGroup>
                            <Label for="Title">
                                Title
                            </Label>
                            <Input
                                id="Title"
                                name="Title"
                                placeholder={post.title}
                                value={post.title}
                                type="text"
                                onChange={(evt) => {
                                    const copy = { ...post };
                                    copy.title = evt.target.value;
                                    editPost(copy);
                                }}
                            />
                        </FormGroup>
                        <FormGroup>
                            <Label for="Content">
                                Content
                            </Label>
                            <Input
                                id="Content"
                                name="Content"
                                placeholder={post.content}
                                value={post.content}
                                type="textarea"
                                onChange={(evt) => {
                                    const copy = { ...post };
                                    copy.content = evt.target.value;
                                    editPost(copy);
                                }}
                            />
                        </FormGroup>
                        <FormGroup>
                            <Label for="ImageLocation">
                                Image URL
                            </Label>
                            <Input
                                id="ImageLocation"
                                name="ImageLocation"
                                placeholder={post.imageLocation}
                                value={post.imageLocation}
                                type="text"
                                onChange={(evt) => {
                                    const copy = { ...post };
                                    copy.imageLocation = evt.target.value;
                                    editPost(copy);
                                }}
                            />
                        </FormGroup>

                        <FormGroup>
                            <Label for="CategoryId">
                                Category
                            </Label>
                            <Input
                                id="CategoryId"
                                name="CategoryId"
                                placeholder={post?.category?.name}
                                value={post.categoryId}
                                type="select"
                                onChange={(evt) => {
                                    const copy = { ...post };
                                    copy.categoryId = evt.target.value;
                                    editPost(copy);
                                }}>

                                <option value="0">Select category</option>
                                {
                                    categories.length > 0 ?
                                        categories.map(
                                            (category) => {
                                                return <option key={category.id} value={category.id}>
                                                    {category.name}
                                                </option>
                                            }
                                        ) : ""
                                }

                            </Input>
                        </FormGroup>

                    </FormGroup>


                </ModalBody>
                <ModalFooter>
                    <Button color="primary" onClick={editP}>
                        Confirm Edit
                    </Button>{" "}
                    <Button color="secondary" onClick={toggle}>
                        Cancel
                    </Button>
                </ModalFooter>
            </Modal>
        </div>
    );
}