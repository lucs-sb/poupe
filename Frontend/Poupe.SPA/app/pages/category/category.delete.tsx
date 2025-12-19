import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  DialogActions,
} from "@mui/material";
import { useSubmit } from "react-router";
import CancelButton from "~/components/buttons/CancelButton";
import DeleteButton from "~/components/buttons/DeleteButton";

type DeleteCategoryProps = {
  open: boolean;
  onClose: () => void;
  id: string;
};

export default function DeleteCategory({ open, onClose, id }: DeleteCategoryProps) {
  const submit = useSubmit();
  
  const handleDelete = () => {
    const fd = new FormData();
    fd.set("id", id);

    submit(fd, { method: "DELETE" });
    handleClose();
  };

  const handleClose = () => {
    onClose();
  };

  return (
    <>
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">{"Deletar categoria ?"}</DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            Tem certeza que deseja deletar essa categoria ?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <CancelButton onClick={handleClose} />
          <DeleteButton onClick={handleDelete} />
        </DialogActions>
      </Dialog>
    </>
  );
}
