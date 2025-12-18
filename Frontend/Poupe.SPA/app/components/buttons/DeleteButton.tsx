import { Button, type ButtonProps } from "@mui/material";

type DeleteButtonProps = {
  label?: string;
  icon?: React.ReactNode;
} & ButtonProps;

export default function DeleteButton({
  label = "Deletar",
    icon = null,
  ...props
}: DeleteButtonProps) {
  return (
    <Button
      variant="contained"
      color="error"
      size="medium"
      startIcon={icon}
      aria-label={label}
      {...props}
    >
      {label}
    </Button>
  );
}
