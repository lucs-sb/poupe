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
import { useCategoriesData } from "~/hooks/useCategoriesData";
import { usePeopleData } from "~/hooks/usePeopleData";

type AddTransactionProps = {
  open: boolean;
  onClose: () => void;
};

export default function AddTransaction({ open, onClose }: AddTransactionProps) {
  const submit = useSubmit();
  const { categories } = useCategoriesData();
  const { people } = usePeopleData();
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
                helperText={
                  errors.value ? (errors.value.message as string) : ""
                }
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
                {errors.type.message ? (errors.type.message as string) : ""}
              </Typography>
            )}
          </FormControl>

          <FormControl
            component="fieldset"
            fullWidth
            margin="normal"
            error={!!errors.categoryId}
          >
            <InputLabel>Categoria</InputLabel>
            <Controller
              name="categoryId"
              control={control}
              defaultValue=""
              rules={{ required: "Informe uma categoria" }}
              render={({ field }) => (
                <Select {...field} label="Categoria" fullWidth displayEmpty>
                  {categories && categories.length > 0 ? (
                    categories.map((category: any) => (
                      <MenuItem key={category.id} value={category.id}>
                        {category.description}
                      </MenuItem>
                    ))
                  ) : (
                    <MenuItem disabled>Sem categorias disponíveis</MenuItem>
                  )}
                </Select>
              )}
            />
            {errors.categoryId && (
              <Typography color="error" variant="body2">
                {errors.categoryId.message
                  ? (errors.categoryId.message as string)
                  : ""}
              </Typography>
            )}
          </FormControl>

          <FormControl
            component="fieldset"
            fullWidth
            margin="normal"
            error={!!errors.userId}
          >
            <InputLabel>Pessoa</InputLabel>
            <Controller
              name="userId"
              control={control}
              defaultValue=""
              rules={{ required: "Informe uma pessoa" }}
              render={({ field }) => (
                <Select {...field} label="Pessoa" fullWidth displayEmpty>
                  {people && people.length > 0 ? (
                    people.map((person: any) => (
                      <MenuItem key={person.id} value={person.id}>
                        {person.name}
                      </MenuItem>
                    ))
                  ) : (
                    <MenuItem disabled>Sem pessoas disponíveis</MenuItem>
                  )}
                </Select>
              )}
            />
            {errors.userId && (
              <Typography color="error" variant="body2">
                {errors.userId.message
                  ? (errors.userId.message as string)
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
