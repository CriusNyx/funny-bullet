import type { PropsWithChildren } from "react";

export type Service = React.ComponentType<PropsWithChildren>;

interface ServiceProviderProps extends PropsWithChildren {
  services: Service[];
}

export function ServiceProvider(props: ServiceProviderProps){
  return props.services.reduceRight((prev, Curr) => (<Curr>{prev}</Curr>), props.children);
}