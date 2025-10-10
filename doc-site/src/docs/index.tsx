import _ from "lodash";
import Concepts from "./Concepts.mdx";
import LevelEditor from "./LevelEditor.mdx";
import Behaviors from "./Behaviors.mdx";
import BulletPatterns from "./BulletPatterns.mdx";

export interface DocInfo {
  name: string;
  component: React.ComponentType;
}

export interface DocRoutes {
  [key: string]: DocInfo | DocRoutes;
}

function doc(name: string, component: React.ComponentType): DocInfo {
  return { name, component };
}

export function isDoc(value: any): value is DocInfo {
  if (!value) {
    return false;
  }
  return "name" in value && "component" in value;
}

export const docRoutes = {
  concepts: doc("Concepts", Concepts),
  levelEditor: doc("Level Editor", LevelEditor),
  behaviors: doc("Behaviors", Behaviors),
  bulletPatterns: doc("BulletPatterns", BulletPatterns),
} satisfies DocRoutes;

export function getDocInfo(path: string): DocInfo | undefined {
  var result = _.get(docRoutes, path);
  if (isDoc(result)) {
    return result;
  }
}
