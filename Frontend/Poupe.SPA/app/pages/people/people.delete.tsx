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

type DeletePeopleProps = {
  open: boolean;
  onClose: () => void;
  id: string;
};

export default function DeletePeople({ open, onClose, id }: DeletePeopleProps) {
  const submit = useSubmit();
  
  const handleClose = () => {
    onClose();
  };

  const handleDelete = () => {
    const fd = new FormData();
    fd.set("id", id);

    submit(fd, { method: "DELETE" });
    handleClose();
  };

  return (
    <>
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">{"Deletar usuário ?"}</DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            Ao deletar esse usuário, todas as transações dele serão apagadas.
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
