import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  DialogActions,
} from "@mui/material";
import CancelButton from "~/components/buttons/CancelButton";
import DeleteButton from "~/components/buttons/DeleteButton";

type DeleteTransactionProps = {
  open: boolean;
  onClose: () => void;
  id: string;
};

export default function DeleteTransaction({ open, onClose, id }: DeleteTransactionProps) {
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
        <DialogTitle id="alert-dialog-title">{"Deletar transação ?"}</DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            Tem certeza que deseja deletar essa transação ?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <CancelButton onClick={handleClose} />
          <DeleteButton onClick={handleClose} />
        </DialogActions>
      </Dialog>
    </>
  );
}
