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
import { useSubmit } from "react-router";
import AddButton from "~/components/buttons/AddButton";
import CancelButton from "~/components/buttons/CancelButton";

type AddCategoryProps = {
  open: boolean;
  onClose: () => void;
};

export default function AddCategory({ open, onClose }: AddCategoryProps) {
  const submit = useSubmit();
  const {
    control,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm();

  const onSubmit = (data: any) => {
    submit(data, { method: "POST" });
    handleClose();
  };

  const handleClose = () => {
    reset();
    onClose();
  };

  return (
    <>
      <Dialog open={open} onClose={handleClose} fullWidth maxWidth="sm">
        <DialogTitle>Cadastrar nova categoria</DialogTitle>
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

          <FormControl
            component="fieldset"
            fullWidth
            margin="normal"
            error={!!errors.purpose}
          >
            <InputLabel>Finalidade</InputLabel>
            <Controller
              name="purpose"
              control={control}
              defaultValue=""
              rules={{ required: "Finalidade é obrigatória" }}
              render={({ field }) => (
                <Select {...field} label="Finalidade" fullWidth displayEmpty>
                  <MenuItem value="0">Receita</MenuItem>
                  <MenuItem value="1">Despesa</MenuItem>
                  <MenuItem value="2">Receita/Despesa</MenuItem>
                </Select>
              )}
            />
            {errors.purpose && (
              <Typography color="error" variant="body2">
                {errors.purpose.message
                  ? (errors.purpose.message as string)
                  : ""}
              </Typography>
            )}
          </FormControl>
        </DialogContent>
        <DialogActions>
          <CancelButton onClick={handleClose} />
          <AddButton onClick={handleSubmit(onSubmit)} />
        </DialogActions>
      </Dialog>
    </>
  );
}
