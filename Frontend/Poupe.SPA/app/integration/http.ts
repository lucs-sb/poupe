type ApiError = {
  status: number;
  message: string;
  details?: unknown;
};

const BASE_URL = import.meta.env.VITE_API_URL as string;

async function parseError(res: Response): Promise<ApiError> {
  let body: any = null;
  try { body = await res.json(); } catch {}
  return {
    status: res.status,
    message: body?.message || "Erro na API",
    details: body,
  };
}

export async function http<T>(path: string, init?: RequestInit): Promise<T> {
  const res = await fetch(`${BASE_URL}${path}`, {
    ...init,
    headers: {
      "Content-Type": "application/json"
    },
  });

  if (!res.ok) throw await parseError(res);

  if (res.status === 204) {
    return {} as T;
  }

  return (await res.json()) as T;
}
