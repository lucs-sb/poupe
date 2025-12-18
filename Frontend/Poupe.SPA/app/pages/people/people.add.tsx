import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
} from "@mui/material";
import { Controller, useForm } from "react-hook-form";
import AddButton from "~/components/buttons/AddButton";
import CancelButton from "~/components/buttons/CancelButton";

type AddPeopleProps = {
  open: boolean;
  onClose: () => void;
};

export default function AddPeople({ open, onClose }: AddPeopleProps) {
  const {
    control,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm();

  const handleClose = () => {
    reset();
    onClose();
  };

  return (
    <>
      <Dialog open={open} onClose={handleClose} fullWidth maxWidth="sm">
        <DialogTitle>Cadastrar novo usuário</DialogTitle>
        <DialogContent>
          <Controller
            name="name"
            control={control}
            defaultValue=""
            rules={{ required: "Nome é obrigatório" }}
            render={({ field }) => (
              <TextField
                {...field}
                fullWidth
                label="Nome"
                margin="normal"
                error={!!errors.name}
                helperText={errors.name ? (errors.name.message as string) : ""}
              />
            )}
          />

          <Controller
            name="age"
            control={control}
            defaultValue=""
            rules={{ required: "Idade é obrigatória" }}
            render={({ field }) => (
              <TextField
                {...field}
                fullWidth
                label="Idade"
                margin="normal"
                type="number"
                inputProps={{
                  min: 1,
                  step: 1,
                }}
                error={!!errors.age}
                helperText={errors.age ? (errors.age.message as string) : ""}
              />
            )}
          />
        </DialogContent>
        <DialogActions>
          <CancelButton onClick={handleClose} />
          <AddButton onClick={handleClose} />
        </DialogActions>
      </Dialog>
    </>
  );
}
