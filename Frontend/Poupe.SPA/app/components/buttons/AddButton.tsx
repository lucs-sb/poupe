import { Button, type ButtonProps } from "@mui/material";
import PlusIcon from "~/icons/PlusIcon";

type AddButtonProps = {
  label?: string;
} & ButtonProps;

export default function AddButton({
  label = "Adicionar",
  ...props
}: AddButtonProps) {
  return (
    <Button
      variant="contained"
      color="success"
      startIcon={<PlusIcon />}
      size="medium"
      aria-label={label}
      {...props}
    >
      {label}
    </Button>
  );
}
