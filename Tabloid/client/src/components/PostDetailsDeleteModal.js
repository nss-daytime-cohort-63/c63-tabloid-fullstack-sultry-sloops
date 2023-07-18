import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "./postDetails.css";

export const PostDetailsDeleteModal = ({ isOpen, toggle, deleteP }) => {
  return (
    <div>
      <Modal isOpen={isOpen} toggle={toggle}>
        <ModalHeader toggle={toggle}>Delete Confirmation</ModalHeader>
        <ModalBody>Are you sure you want to delete this post?</ModalBody>
        <ModalFooter>
          <Button color="danger" onClick={deleteP}>
            Confirm Delete
          </Button>{" "}
          <Button color="secondary" onClick={toggle}>
            Cancel
          </Button>
        </ModalFooter>
      </Modal>
    </div>
  );
};
