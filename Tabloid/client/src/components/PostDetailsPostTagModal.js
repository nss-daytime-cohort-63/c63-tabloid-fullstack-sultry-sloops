import {
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  FormGroup,
  Input,
  Label,
} from "reactstrap";

import "./postDetails.css";

export const PostDetailsPostTagModal = ({
  isOpen,
  toggle,
  tags,
  submit,
  setNewPostTag,
  newPostTag,
}) => {
  return (
    <div>
      <Modal isOpen={isOpen} toggle={toggle}>
        <ModalHeader toggle={toggle}>Add a Tag to Post</ModalHeader>
        <ModalBody>
          <FormGroup>
            {tags.map((t) => {
              return (
                <>
                  <div>
                    <FormGroup>
                      <Input
                        bsSize=""
                        type="radio"
                        name="tag"
                        onClick={() => {
                          const copy = { ...newPostTag };
                          copy.tagId = t.id;
                          setNewPostTag(copy);
                        }}
                      />
                      <Label>
                        {t.name} - {t.id}
                      </Label>
                    </FormGroup>
                  </div>
                </>
              );
            })}
          </FormGroup>
        </ModalBody>
        <ModalFooter>
          <Button color="primary" onClick={submit}>
            Submit Post Tag
          </Button>{" "}
          <Button color="secondary" onClick={toggle}>
            Cancel
          </Button>
        </ModalFooter>
      </Modal>
    </div>
  );
};
