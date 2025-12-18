import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  FormControl,
  InputLabel,
  Typography,
  MenuItem,
  Select,
} from "@mui/material";
import { Controller, useForm } from "react-hook-form";
import AddButton from "~/components/buttons/AddButton";
import CancelButton from "~/components/buttons/CancelButton";

type AddTransactionProps = {
  open: boolean;
  onClose: () => void;
};

export default function AddTransaction({ open, onClose }: AddTransactionProps) {
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
        <DialogTitle>Cadastrar nova transação</DialogTitle>
        <DialogContent>
          <Controller
            name="description"
            control={control}
            defaultValue=""
            rules={{ required: "Descrição é obrigatória" }}
            render={({ field }) => (
              <TextField
                {...field}
                fullWidth
                label="Descrição"
                margin="normal"
                error={!!errors.description}
                helperText={
                  errors.description
                    ? (errors.description.message as string)
                    : ""
                }
              />
            )}
          />

          <Controller
            name="value"
            control={control}
            defaultValue=""
            rules={{ required: "Valor é obrigatório" }}
            render={({ field }) => (
              <TextField
                {...field}
                fullWidth
                label="Valor"
                margin="normal"
                type="number"
                inputProps={{
                  min: 0,
                  step: 0.01,
                }}
                error={!!errors.value}
                helperText={errors.value ? (errors.value.message as string) : ""}
              />
            )}
          />

          <FormControl
            component="fieldset"
            fullWidth
            margin="normal"
            error={!!errors.type}
          >
            <InputLabel>Tipo</InputLabel>
            <Controller
              name="type"
              control={control}
              defaultValue=""
              rules={{ required: "Tipo é obrigatório" }}
              render={({ field }) => (
                <Select {...field} label="Tipo" fullWidth displayEmpty>
                  <MenuItem value="0">Receita</MenuItem>
                  <MenuItem value="1">Despesa</MenuItem>
                </Select>
              )}
            />
            {errors.type && (
              <Typography color="error" variant="body2">
                {errors.type.message
                  ? (errors.type.message as string)
                  : ""}
              </Typography>
            )}
          </FormControl>
        </DialogContent>
        <DialogActions>
          <CancelButton onClick={handleClose} />
          <AddButton onClick={handleClose} />
        </DialogActions>
      </Dialog>
    </>
  );
}
