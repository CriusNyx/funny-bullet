interface Props {
  className?: string;
}

export function HBar(props: Props) {
  return <div className={`bg-a h-px w-full ${props.className}`} />;
}
